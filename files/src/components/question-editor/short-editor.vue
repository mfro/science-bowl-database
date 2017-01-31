<template lang="html">
    <div class="short-editor">
        <textarea class="content" v-model="data.answer" ref="content"/>
    </div>
</template>

<script>
    function auto(e) {
        e.style.height = 0;
        e.style.height = (e.scrollHeight + 10) + 'px';
    }

    export default {
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

    .short-editor {
        margin-top: 10px;
    }

    .content {
        width: 100%;
        resize: none;
        font-size: inherit;
        font-family: Roboto Mono;
    }
</style>
