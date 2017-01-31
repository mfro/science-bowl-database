<template lang="html">
    <div class="question-editor">
        <question-editor-header :data="data" :group="group"/>
        
        <div class="body">
            <textarea class="content" v-model="data.content" ref="content"/>

            <choice-editor v-if="data.type == 'choice'" :data="data" :group="group"/>
            <short-editor v-else :data="data" :group="group"/>
        </div>
    </div>
</template>

<script>
    import { REST } from 'questions';

    import QuestionEditorHeader from './question-editor-header';
    import ChoiceEditor from './choice-editor';
    import ShortEditor from './short-editor';

    function auto(e) {
        e.style.height = 0;
        e.style.height = (e.scrollHeight + 10) + 'px';
    }

    export default {
        components: {
            ShortEditor,
            ChoiceEditor,
            QuestionEditorHeader,
        },

        props: ['data', 'group'],

        mounted() {
            auto(this.$refs.content);
        },

        updated() {
            auto(this.$refs.content);
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";
    
    .question-editor {
        display: flex;
        flex-direction: column;

        min-width: 6in;

        background: white;
        box-shadow: 1px 5px 20px gray;
    }

    .body {
        padding: 10px;
    }

    .content {
        width: 100%;
        padding: 8px;
        resize: none;
        font-size: inherit;
        font-family: Roboto Mono;
    }
</style>
