import '!style-loader?insertAt=top!css-loader!./reset.css';
import './index.html';

import Vue from 'vue';
import App from './app';
import { REST } from 'questions';

REST.load(() => {
    var app = new Vue({
        el: '#react',
        render: h => h('app'),
        components: { App }
    });
});