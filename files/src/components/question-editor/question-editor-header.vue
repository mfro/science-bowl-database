<template lang="html">
    <div class="question-editor-header">
        <div class="top">
            <h2 @click="data.tossup = !data.tossup">{{ data.tossup ? 'Toss-Up' : 'Bonus' }}</h2>
            
            <div class="spacer"/>

            <h2 @click="toggleAnswerType()">{{ data.type == 'short' ? 'Short Answer' : 'Multiple Choice' }}</h2>
        </div>

        <my-select class="categories" v-model="data.category" :options="group.categories"/>
    </div>
</template>

<script>
    import MySelect from './my-select';

    export default {
        components: { MySelect },

        props: ['data', 'group'],

        methods: {
            toggleAnswerType() {
                this.data.type = this.data.type == 'choice' ? 'short' : 'choice';

                this.$forceUpdate();
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .question-editor-header {
        display: flex;
        flex-direction: column;
        padding: 10px 10px 0 10px;

        font-size: 1.1em;
        font-weight: bold;
    }

    .top {
        display: flex;
        margin-bottom: 10px;

        h2 {
            flex: 1;
        }
    }

    .spacer {
        flex: 0 0 10px;
    }

    select, h2 {
        outline: none;
        cursor: pointer;
        border-bottom: 1px dotted grey;
    }

    .categories {
        font-family: inherit;
        font-weight: bold;
        font-size: inherit;
        -webkit-appearance: none;
        background: none;
        border: none;
        border-bottom: 1px dotted grey;
    }
</style>
