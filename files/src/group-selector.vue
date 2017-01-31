<template lang="html">
    <div class="group-selector">
        <div class="list" v-if="groups">
            <div class="group-selector-item" v-for="group in groups" @click="$emit('input', group)">
                <span>{{ group.name }}</span>
            </div>
        </div>
        <span v-else>Loading...</span>
    </div>
</template>

<script>
    import { REST } from 'questions';

    export default {
        data() {
            return {
                groups: null
            };
        },

        created() {
            REST('GET', '/groups').then(lists => {
                for (var group of lists)
                    if (group.name.toUpperCase() == window.location.hash.toUpperCase().substring(1))
                        this.$emit('input', group);
                this.groups = lists;
            });
        }
    }
</script>

<style scoped lang="less">
    div.group-selector {
        padding: 10px;

        > .list {
            display: flex;
            flex-direction: column;
            align-items: flex-start;
            margin: 10px;
        }

        .group-selector-item {
            padding: 10px 20px 8px;
            cursor: pointer;
            margin-bottom: 20px;
            transition: all 150ms;
            text-align: center;
            box-shadow: 0 0 10px gray;
        }
    }
</style>
