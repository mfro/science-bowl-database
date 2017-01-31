<template>
    <div class="clock-timer">
        <div class="left">
            <span class="reset" @click="reset()"/>
        </div>
        {{ minutes }}:{{ seconds }}
        <div class="right">
            <span class="pauseplay" :class="{ running }" @click="toggle()"/>
        </div>
    </div>
</template>

<script>
    export default {
        data() {
            return {
                time: 300,
                running: false
            };
        },

        computed: {
            minutes() {
                return Math.floor(this.time / 60);
            },

            seconds() {
                var str = Math.floor(this.time % 60).toFixed(0);
                if (str.length == 1) str = '0' + str;
                return str;
            }
        },

        created() {
            requestAnimationFrame(this.tick);
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
                this.time = 300;
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
    .clock-timer {
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