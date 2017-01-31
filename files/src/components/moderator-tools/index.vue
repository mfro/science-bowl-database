<template>
    <div class="moderator-tools">
        <clock-timer/>
        <bonus-timer/>
        <scoreboard/>
    </div>
</template>

<script>
    import ClockTimer from './clock-timer';
    import BonusTimer from './bonus-timer';
    import Scoreboard from './scoreboard';

    export default {
        components: {
            ClockTimer,
            BonusTimer,
            Scoreboard
        },
    };
</script>

<style scoped lang="less">
    @import "~vars.less";

    .moderator-tools {
        flex: 0 0 400px;
        display: flex;
        flex-direction: column;
        background: white;
        box-shadow: 0 0 10px gray;
        z-index: 1;
    }
</style>