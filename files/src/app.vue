<template lang="html">
    <div class="questions-app">
        <group v-if="group" :group="group" @closed="select(null)"/>
        <group-selector v-else @input="select($event)"/>
    </div>
</template>

<script>
    import GroupSelector from './group-selector';
    import Group from 'pages/group';

    export default {
        components: {
            Group,
            GroupSelector,
        },

        data() {
            return {
                group: null
            }
        },

        methods: {
            select(group) {
                if (group) {
                    window.location.hash = group.name;
                    document.title = group.name;
                } else {
                    window.location.hash = '';
                    document.title = 'Question Database';
                }

                this.group = group;
            }
        }
    }
</script>

<style lang="less">
    body {
        font-family: Roboto;
        margin: 0;
    }

    sub {
        font-size: smaller;
        position: relative;
        top: 0.1em;
        vertical-align: top;
    }

    sup {
        font-size: smaller;
        position: relative;
        top: -0.4em;
        vertical-align: top;
    }

    button, input[type="button"] {
        font-family: inherit;
        padding: 8px 20px;
        font-size: 16px;
        border: none;
        outline: none;
        cursor: pointer;
        background: #F3F3F3;
        color: #313131;
        border-radius: 3px;
        border: 1px solid lightgray;
        transition: background 150ms ease-in-out;
        
        &:hover {
            background: #E0E0E0;
        }

        &[disabled] {
            color: gray;
            pointer-events: none;
        }
    }
</style>

<style scoped lang="less">
    div.questions-app {
        width: 100%;
        height: 100%;
    }
</style>
