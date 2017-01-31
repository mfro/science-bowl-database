<template lang="html">
    <label class="query-toggle">
        <input class="input" type="checkbox" :checked="checked" @change="onChange(!checked)"/>
        {{ label }}
    </label>
</template>

<script>
    export default {
        props: ['label', 'value', 'list', 'id'],

        computed: {
            checked() {
                if (this.list)
                    return this.list.indexOf(this.id) != -1;
                return this.value;
            }
        },

        methods: {
            onChange(value) {
                if (this.list){
                    if (value)
                        this.list.push(this.id);
                    else
                        this.list.splice(this.list.indexOf(this.id), 1);    
                }

                this.$emit('input', value);
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .query-toggle {
        flex: 1 0 auto;
        padding: 4px;
        display: flex;
        align-items: flex-start;

        > input {
            margin: 0 5px;
        }
    }

    .input {
        -webkit-appearance: none;
        background-color: white;
        width: 18px;
        height: 18px;
        border-radius: 2px;
        position: relative;

        border: 2px solid gray;
        outline: none;
        cursor: pointer;

        &:focus {
            box-shadow: 0 0 10px gray;
        }

        &:checked {
            border-width: 0;
            background-color: @theme-color;

            &:before {
                content: ' ';
                background: white;
                position: absolute;
                top: 3px;
                left: 10px;
                width: 2px;
                height: 12px;
                transform: rotate(45deg);
            }

            &:after {
                content: ' ';
                background: white;
                position: absolute;
                top: 7px;
                left: 4px;
                width: 2px;
                height: 6.2px;
                transform: rotate(-45deg);
            }
        }
    }
</style>
