<template lang="html">
    <div class="question-body">
        <p class="content" v-html="md(data.content)"/>
        <br />
        <p class="answer">
            <template v-if="data.type == 'short'">
                <span v-if="showAnswer" v-html="md(data.answer)" />
            </template>
            <template v-else v-for="(opt, i) in data.answers">
                <div class="option" :class="{ correct: showAnswer && i == data.correct }">
                    <span class="letter">{{ ['W', 'X', 'Y', 'Z'][i] }}: </span>
                    <span class="text" v-html="md(opt)"/>
                </div>
            </template>
        </p>
    </div>
</template>

<script>
    var markdown = new Remarkable();
    markdown.inline.ruler.enable(['sup', 'sub']);

    export default {
        props: ['data', 'group', 'showAnswer'],

        methods: {
            md(raw) {
                return markdown.render(raw);
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .question-body {
        flex: 1;
        position: relative;
    }

    p {
        padding: 0 6px;
        vertical-align: text-bottom;
    }

    .option {
        display: flex;
        flex-wrap: wrap;

        > .text {
            flex: 1;
        }

        &.correct {
            text-shadow: 0 0 10px lightgreen;
            color: darkgreen;
        }
    }

    .letter {
        font-family: Roboto Mono;
        position: relative;
        top: -1px;
    }
</style>
