<template lang="html">
    <div class="random-page">
        <div class="controls">
            <h1 class="header">Random Questions</h1>

            <query-builder :group="group" v-model="query"/>

            <div class="buttons">
                <button class="submit-button" @click="onRandom()">Randomize!</button>
                <button class="submit-button" @click="onMatching()" :disabled="!history[0]">Get Matching Bonus</button>
            </div>

            <div class="spacer" />

            <div class="settings">
                <div class="row">
                    <query-toggle label="Show Moderator Tools" v-model="showTools"/>
                </div>
                <div class="row">
                    <query-toggle label="Show Answers" v-model="answers"/>
                    <query-toggle label="Keep History" v-model="keepHistory"/>
                </div>

                <button class="submit-button" @click="clear()">Clear History</button>
            </div>
        </div>
        <div class="history">
            <question v-for="que in history" :data="que" :group="group" :key="que.id" :answer="answers"/>
        </div>
        <moderator-tools v-if="showTools"/>
    </div>
</template>

<script>
    import { REST } from 'questions';
    
    import ModeratorTools from 'components/moderator-tools';
    import QueryBuilder from 'components/query-builder';
    import QueryToggle from 'components/query-builder/query-toggle';

    import Question from 'components/editable-question';

    var skipQuestions = [];

    export default {
        components: {
            ModeratorTools,
            QueryBuilder,
            QueryToggle,
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
                history: [],
                answers: false,
                showTools: true,
                keepHistory: true,
            }
        },

        methods: {
            onRandom() {
                this.doQuery(this.query);
            },

            onMatching() {
                this.doQuery(Object.assign({}, this.query, {
                    bonus: true,
                    tossup: false,
                    categories: [this.history[0].category]
                }));
            },


            doQuery(query) {
                console.log(query);
                query.skip = skipQuestions;
                REST('POST', `/groups/${this.group.id}/questions/search?random=true`, query).then(res => {
                    skipQuestions.push(res.data.id);

                    if (this.keepHistory)
                        this.history.unshift(res.data);
                    else
                        this.history = [res.data];
                }, e => {
                    console.log(e);
                });
            },

            clear() {
                this.history = [];
                skipQuestions = [];
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .random-page {
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
        padding-right: 10px;
        display: flex;

        > button {
            margin-left: 10px;
            flex: 1 auto;
        }
    }


    .settings {
        display: flex;
        flex-direction: column;

        > button {
            margin: 10px;
        }

        label.checkbox {
            flex: 1 0 auto;
            display: flex;
            align-items: center;
            padding: 4px;

            > input {
                margin: 0 5px;
            }
        }

        .row {
            display: flex;
        }
    }

    .history {
        flex: 1;
        padding-bottom: 20px;
        overflow-y: auto;
    }

    .spacer {
        flex: 1;
    }
</style>
