<template lang="html">
    <div class="nav-item" :class="{ active }" @click="$emit('nav')">
        <span>{{ label }}</span>
    </div>
</template>

<script>
    export default {
        props: ['active', 'label']
    }
</script>

<style scoped lang="less">
    @import "~vars.less";

    div.nav-item {
        margin: 0 10px;
        padding: 14px 14px 10px 14px;
        cursor: pointer;
        transition: border-color 200ms;
        border-bottom: 4px solid white;
        border-color: transparent;

        &:hover {
            border-color: fade(white, 70%) ;
        }

        &.active {
            border-color: white;
        }
    }
</style>
