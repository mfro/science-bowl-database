<template lang="html">
    <div class="question" :style="{ lineHeight: lineHeight + 'px' }">
        <question-header :data="data" :group="group" :buttons="(show || answer) && !decorative" @editing="$emit('editing')"
                         @click.native="onClick($event)"/>
        
        <div class="body" @click="onClick($event)">
            <svg class="lines">
                <template v-for="i in 13">
                    <line x1="0" :y1="Math.ceil(i * lineHeight) + .5" x2="10000" :y2="Math.ceil(i * lineHeight) + .5"/>
                </template>
            </svg>
            <div :style="{ height: (lineHeight * .25) + 'px' }"/>

            <question-display-body :data="data" :group="group" :showAnswer="show || answer"/>
        </div>
    </div>
</template>

<script>
    import { REST } from 'questions';

    import QuestionHeader from './question-header';
    import QuestionDisplayBody from './question-display-body';

    export default {
        components: {
            QuestionHeader,
            QuestionDisplayBody
        },

        props: {
            data: Object,
            group: Object,
            answer: { type: Boolean, default: false },
            decorative: { type: Boolean, default: false }
        },

        data() {
            return {
                lineHeight: 0,
                show: false
            }
        },

        mounted() {
            this.lineHeight = Math.ceil(this.$el.clientHeight / 15);
        },

        methods: {
            onClick(e) {
                var node = e.target;
                while (node) {
                    if (node.classList && node.classList.contains('buttons'))
                        return;
                    node = node.parentNode;
                }

                this.show = !this.show;
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .question {
        display: inline-flex;
        margin: 20px 0 0 20px;
        width: 6in;
        height: 4in;

        background: white;
        box-shadow: 1px 5px 20px gray;

        position: relative;
        flex-direction: column;
        
        cursor: pointer;
    }

    .body {
        flex: 1;
        position: relative;

        display: flex;
        flex-direction: column;
    }

    .lines {
        position: absolute;
        width: 100%;
        height: 100%;
        stroke: blue;
        top: 0;
        left: 0;
        pointer-events: none;
    }
</style>
