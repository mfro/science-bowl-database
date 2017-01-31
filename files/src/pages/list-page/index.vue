<template lang="html">
    <div class="list-page">
        <div class="controls">
            <h1 class="header">List Questions</h1>

            <query-builder :group="group" v-model="query"/>

            <div class="buttons">
                <button class="submit-button" @click="update(start)">Update List</button>
            </div>

            <div class="page-controls">
                <button class="submit-button" @click="page(-1)">{{ '<' }}</button>
                <span>{{ start + 1 }} - {{ start + list.length }} of {{ total }}</span>
                <button class="submit-button" @click="page(+1)">{{ '>' }}</button>
            </div>

            <div class="spacer"/>
        </div>
        <div class="list">
            <question v-for="que in list" :data="que" :group="group" :key="que.id" :answer="true"/>
        </div>
    </div>
</template>

<script>
    import { REST } from 'questions';

    import QueryBuilder from 'components/query-builder';
    import Question from 'components/editable-question';

    const pageSize = 20;

    export default {
        components: {
            QueryBuilder,
            Question,
        },
        
        props: ['group'],

        data() {
            var categories = [];
            for (var { id } of this.group.categories)
                categories.push(id);
            
            return {
                query: {
                    categories,
                    bonus: true,
                    tossup: true,
                    flagged: false,

                    short: true,
                    choice: true,

                    search: ''
                },
                list: [],
                start: 0,
                total: 0,
            }
        },

        methods: {
            update(start) {
                var query = Object.assign({
                    start: start,
                    count: pageSize
                }, this.query);

                REST('POST', `/groups/${this.group.id}/questions/search`, query).then(res => {
                    this.list = res.data;
                    this.total = res.total;
                }, e => {
                    console.log(e);
                });
            },

            page(offset) {
                var start = this.start + offset / Math.abs(offset) * pageSize;
                start = Math.max(start, 0);
                if (start > this.total || start == this.start) return;
                this.start = start;

                if (this.timeout) clearTimeout(this.timeout);
                this.timeout = setTimeout(() => {
                    this.update(start);
                }, 300);
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .list-page {
        flex: 1;
        display: flex;
    }

    .header {
        font-size: 24px;
        font-weight: bold;
        margin: 10px;
    }

    .controls {
        flex: 0 0 auto;
        display: flex;
        flex-direction: column;
        background: white;
        box-shadow: 0 0 10px gray;
        z-index: 1;
    }

    .buttons {
        padding: 0 10px;

        > button {
            width: 100%;
        }
    }

    .page-controls {
        margin: 10px;
        display: flex;
        align-items: center;
        justify-content: space-between;
    }

    .list {
        flex: 1;
        padding-bottom: 20px;
        overflow-y: auto;
    }

    .spacer {
        flex: 1;
    }
</style>
