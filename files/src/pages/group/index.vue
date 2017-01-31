<template lang="html">
    <div class="group">
        <div class="header">
            <nav-item :active="page == 'query'" label="Random Questions" @nav="page = 'query'" />
            <nav-item :active="page == 'create'" label="Create Questions" @nav="page = 'create'" />
            <nav-item :active="page == 'list'" label="List Questions" @nav="page='list'" />
            <div class="spacer"/>
            <nav-item :active="false" label="Back" @nav="$emit('closed')" />
        </div>

        <random-page v-if="page == 'query'" :group="group"/>
        <create-page v-else-if="page == 'create'" :group="group"/>
        <list-page v-else-if="page == 'list'" :group="group"/>
    </div>
</template>

<script>
    import NavItem from './nav-item';
    import RandomPage from 'pages/random-page';
    import CreatePage  from 'pages/create-page';
    import ListPage from 'pages/list-page';

    export default {
        components: {
            NavItem,
            RandomPage,
            CreatePage,
            ListPage
        },

        props: ['group'],

        data() {
            return {
                page: 'query',
            };
        }
    };
</script>

<style scoped lang="less">
    @import "~vars.less";

    .group {
        width: 100%;
        height: 100%;
        display: flex;
        flex-direction: column;
    }

    .header {
        display: flex;
        background: @theme-color;
        color: white;
        box-shadow: 0 0 10px gray;
        z-index: 2;
        position: relative;
    }

    .spacer {
        flex: 1;
    }
</style>
