<template>
    <div class="bonus-timer" :class="{ close: time < 5 }">
        <div class="left">
            <span class="reset" @click="reset()"/>
        </div>
        {{ time.toFixed(1) }}
        <div class="right">
            <span class="pauseplay" :class="{ running }" @click="toggle()"/>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                time: 20,
                running: false
            };
        },

        created() {
            requestAnimationFrame(this.tick);
        },

        beforeDestroy() {
            if (this.intervalId)
                clearInterval(this.intervalId);
        },

        methods: {
            tick() {
                if (this.running) {
                    let now = performance.now();
                    this.time -= (now - this.lastNow) / 1000;
                    this.lastNow = now;
                }

                if (this.time < 0) {
                    this.reset();
                }

                if (!this._isDestroyed)
                    requestAnimationFrame(this.tick);
            },

            reset() {
                this.time = 20;
                this.running = false;
            },

            toggle() {
                this.running = !this.running;
                this.lastNow = performance.now();
            }
        }
    };
</script>

<style scoped lang="less">
    .bonus-timer {
        display: flex;
        font-size: 1.5em;
    }

    .left {
        flex-direction: row-reverse;
    }
    .left, .right {
        flex: 1;
        display: flex;
        padding: 0 20px;
    }

    .time {
        display: flex;
        font-size: 2em;
        padding: 20px 0;

        .left {
            flex-direction: row-reverse;
        }

        .left, .right {
            flex: 1;
            display: flex;
            padding: 0 20px;
            align-items: center;
        }
    }

    .reset {
        width: 20px;
        height: 20px;
        cursor: pointer;

        background-image: url(reset.png);
        background-position: center;
        background-repeat: no-repeat;
        background-size: contain;
    }

    .pauseplay {
        width: 20px;
        height: 20px;
        cursor: pointer;

        background-size: contain;
        background-position: center;
        background-repeat: no-repeat;
        background-image: url(play.svg);
        &.running {
            background-image: url(pause.svg);
        }
    }
</style>