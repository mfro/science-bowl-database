<template lang="html">
    <div class="question-header">
        <div class="left">
            <h2>{{ data.tossup ? 'Toss-Up' : 'Bonus' }}</h2>
            <h2>{{ group.categories.find(c => c.id == data.category).name }}</h2>
        </div>
        <div class="right">
            <h2>{{ data.type == 'short' ? 'Short Answer' : 'Multiple Choice' }}</h2>

            <div class="buttons" v-show="buttons">
                <span @click="toggleFlag()" class="flag-button">
                    <svg
                        version="1.1" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100"
                        :style="{ stroke: flagColor, fill: flagColor }">
                        <line x1="5" y1="20" x2="22" y2="90" stroke-width="10" />
                        <path d="m 20 20 c 20 6 40 -10 60 -10 l 8.5 35 c -20 0 -40 16 -60 10 z" stroke-width="5" />
                    </svg>
                </span>
                <span @click="toggleEdit()" class="edit-button"/>
            </div>
        </div>
    </div>
</template>

<script>
    import { REST } from 'questions';

    export default {
        props: ['data', 'group', 'buttons'],

        computed: {
            flagColor() {
                return this.data.flagged ? 'red' : 'black'
            }
        },

        methods: {
            toggleFlag() {
                this.data.flagged = !this.data.flagged;
                REST('PUT', `/groups/${this.group.id}/questions/${this.data.id}`, this.data).then(e => {
                    Object.assign(this.data, e);

                    this.$forceUpdate();
                });
            },

            toggleEdit() {
                this.$emit('editing');
                this.$forceUpdate();
            }
        }
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    .question-header {
        border-bottom: 1px solid red;
        display: flex;
        justify-content: space-between;
        padding: 2px 6px;
        box-sizing: border-box;

        font-size: 1.1em;
        font-weight: bold;
    }

    h2 {
        pointer-events: none;
    }

    .left, .right {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        align-items: flex-start;
    }

    .right {
        align-items: flex-end;
        padding-bottom: 2px;
        
        > .buttons {
            display: flex;
        }
    }

    .edit-button {
        background: url(edit.svg) center/contain;
        width: 18px;
        height: 18px;
        opacity: .5;
        cursor: pointer;

        &:hover {
            opacity: 1;
        }
    }

    .flag-button {
        width: 18px;
        height: 18px;
        opacity: .5;
        cursor: pointer;
        margin: 0 10px;

        &:hover {
            opacity: 1;
        }

        > svg {
            fill: black;
            stroke: black;
            stroke-linecap: round;
            stroke-linejoin: round;

            > line {
                stroke-width: 10px;
            }

            > path {
                stroke-width: 5px;
            }
        }
    }
</style>
