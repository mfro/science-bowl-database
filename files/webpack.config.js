const path = require("path");
const { DefinePlugin } = require('webpack');

const config = {
    entry: './src/index.js',
    output: {
        path: path.join(__dirname, 'build'),
        filename: "bundle.js"
    },
    resolve: {
        modules: ['src', 'node_modules'],
        extensions: [".js", ".vue"],
        alias: {
            'vue$': 'vue/dist/vue.js',
        }
    },
    module: {
        rules: [
            {
                test: /\.js$/,
                loader: 'babel-loader',
                options: { presets: ['es2015'] },
            },
            {
                test: /\.html$/,
                loader: 'file-loader',
                options: { name: '[name].[ext]' },
            },
            {
                test: /\.svg$/,
                loader: 'url-loader',
                options: { limit: 10000 },
            },
            {
                test: /\.png$/,
                loader: 'url-loader',
                options: { limit: 10000 },
            },
            {
                test: /\.vue$/,
                loader: "vue-loader",
                options: {
                    loaders: {
                        less: "style-loader!css-loader!less-loader?strictMath"
                    }
                }
            },
        ]
    },
    plugins: []
};

module.exports = function (env) {
    let args = JSON.parse(process.env.DEPLOY_ARGS || '{}');
    
    let service = args.api_host || 'localhost:8080';

    console.log('weback build args:');
    console.log('  SERVICE_URL:', service);

    config.plugins.push(new DefinePlugin({
        'SERVICE_URL': JSON.stringify(service),
    }));

    return config;
}