<template lang="html">
    <select ref="select" @input="onInput($event.target.value)">
        <option v-for="{ name, id } of options" :value="id">{{ name }}</option>
    </select>
</template>

<script>
    export default {
        props: ['value', 'options'],

        mounted() {
            this.$refs.select.value = this.value;
        },

        data() {
            return {
                edited: {}
            }
        },

        watch: {
            value(v) {
                if (!this.$refs.select) return;
                this.$refs.select.value = this.value;
            }
        },

        methods: {
            onInput(value) {
                this.$emit('input', value);
            }
        }
    }
</script>

<style scoped lang="less">
</style>
