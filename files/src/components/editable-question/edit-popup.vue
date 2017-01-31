<template lang="html">
    <div class="editor-container">
        <div class="column">
            <question-editor :data="data" :group="group"/>

            <div class="footer">
                <button @click="onCancel()">Cancel</button>
    
                <div class="spacer"/>

                <button @click="onSave()">Save</button>
            </div>
        </div>
        <div class="column">
            <question decorative :data="data" :group="group" :answer="true" style="pointer-events: none; margin: 0"/>
        </div>
    </div>
</template>

<script>
    import { REST } from 'questions';

    import Question from 'components/question';
    import QuestionEditor from 'components/question-editor';

    function diff(o, n) {
        for (var key in o) {
            if (['answer', 'answers', 'correct'].indexOf(key) != -1) continue;

            if (o[key] != n[key]) return true;
        }

        if (o.type == 'choice') {
            if (o.correct != n.correct) return true;
            if (!o.answers || !n.answers) return true;

            for (var i = 0; i < o.answers.length; i++) {
                if (o.answers[i] != n.answers[i]) return true;
            }
        } else {
            if (o.answer != n.answer) return true;
        }
    }

    export default {
        components: {
            Question,
            QuestionEditor
        },

        props: ['data', 'group'],

        methods: {
            onCancel() {
                this.$emit('done', null);
            },

            onSave() {
                REST('PUT', `/groups/${this.group.id}/questions/${this.data.id}`, this.data).then(e => {
                    this.$emit('done', e);
                });
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .editor-container {
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        z-index: 100;

        display: flex;
        align-items: flex-start;
        justify-content: center;

        background: rgba(0, 0, 0, 0.4);
    }

    .column {
        display: flex;
        flex-direction: column;

        margin: 10px 5px;

        animation: slidein 300ms;

        @keyframes slidein {
            0%   { transform: translate(0, -100%) }
            100% { transform: translate(0, 0) }
        }
    }

    .footer {
        display: flex;
        padding: 10px;
        background: white;
        margin-top: 10px;

        > button {
            flex: 1;
        }
    }

    .spacer {
        flex: 0 0 10px;
    }
</style>
