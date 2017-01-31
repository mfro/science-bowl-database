<template lang="html">
    <div class="query-builder">
        <div class="row">
            <query-toggle v-model="value.bonus" label="Bonus"/>
            <query-toggle v-model="value.tossup" label="Toss-Up"/>
        </div>
        <br />
        <div class="categories">
            <template v-for="{ name, id } of group.categories">
                <query-toggle :label="name" :list="value.categories" :id="id"/>
            </template>
        </div>
        <br />
        <div class="row">
            <query-toggle v-model="value.choice" label="Multiple Choice"/>
            <query-toggle v-model="value.short" label="Short Answer"/>
        </div>
        <br />
        <div class="row">
            <query-toggle v-model="value.flagged" label="Flagged"/>
        </div>
        <div class="search">
            <span>Search: </span>
            <input type="text" v-model="value.search"/>
        </div>
    </div>
</template>

<script>
    import QueryToggle from './query-toggle';

    export default {
        components: { QueryToggle },

        props: ['group', 'value'],

        watch: {
            value(n, o) {
                console.log(n);
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .query-builder {
        min-width: 350px;
    }

    .row {
        display: flex;
    }

    .search {
        padding: 10px;
        
        > input {
            width: 100%;
        }
    }
</style>
