<template lang="html">
    <div class="editable-question">
        <question :data="data" :group="group" :answer="answer" @editing="onEditing()"/>

        <edit-popup v-if="editing" :data="modified" :group="group" @done="onDoneEditing($event)"/>
    </div>
</template>

<script>
    import { REST } from 'questions';

    import Question from 'components/question';
    import EditPopup from './edit-popup';

    function clone(v) {
        var c = {};
        for (var key in v) {
            if (v[key] instanceof Array)
                c[key] = v[key].slice();
            else
                c[key] = v[key];
        }
        return c;
    }

    export default {
        components: {
            Question,
            EditPopup
        },

        props: {
            data: Object,
            group: Object,
            answer: { type: Boolean, default: false }
        },

        data() {
            return {
                modified: null,
                editing: false,
            }
        },

        mounted() {
            this.lineHeight = Math.ceil(this.$el.clientHeight / 15);
        },

        methods: {
            onEditing() {
                this.data.answer = this.data.answer || '';
                this.data.answers = this.data.answers || ['', '', '', ''];

                this.modified = clone(this.data);
                this.editing = true;
            },

            onDoneEditing(arg) {
                if (arg != null) {
                    Object.assign(this.data, arg);
                }

                this.modified = null;
                this.editing = false;
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .editable-question {
        display: inline-flex;
    }
</style>
