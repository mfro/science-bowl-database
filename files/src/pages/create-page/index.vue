<template lang="html">
    <div class="create-page">
        <div class="column">
            <question-editor :data="data" :group="group"/>

            <div class="footer">
                <button @click="onCreate()">Create</button>
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

    function getData(g) {
        return {
            tossup: true,
            flagged: false,
            content: 'Blank question',
            answer: 'Blank answer',
            type: 'choice',
            category: g.categories[0].id,
            answers: [
                'Answer 1',
                'Answer 2',
                'Answer 3',
                'Answer 4'
            ],
            correct: 0
        };
    }

    export default {
        components: {
            Question,
            QuestionEditor,
        },
        
        props: ['group'],

        data() {
            return {
                data: getData(this.group)
            }
        },

        methods: {
            onCreate() {
                REST('POST', `/groups/${this.group.id}/questions`, this.data).then(e => {
                    this.data = getData(this.group);
                });
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .create-page {
        display: flex;
    }

    .column {
        display: flex;
        flex-direction: column;
        margin: 20px 0 20px 20px;
    }

    .footer {
        margin: 20px 0;
    }
</style>
