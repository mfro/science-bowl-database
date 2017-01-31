<template lang="html">
    <div class="choice-editor">
        <div class="option" :class="{ correct: i == data.correct }" v-for="(opt, i) in data.answers">
            <span class="letter" @click="data.correct = i">{{ ['W', 'X', 'Y', 'Z'][i] }}: </span>
            <input class="text" type="text" v-model="data.answers[i]"/>
        </div>
    </div>
</template>

<script>
    export default {
        props: ['data', 'group'],
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .choice-editor {
        display: f
    }

    .option {
        display: flex;
        flex-wrap: wrap;
        margin-top: 10px;
        font-family: Roboto Mono;
        align-items: center;

        &.correct {
            text-shadow: 0 0 10px lightgreen;
            color: darkgreen;
        }
    }

    .text {
        flex: 1;
        font-size: inherit;
        font-family: inherit;
    }

    .letter {
        cursor: pointer;
    }
</style>
