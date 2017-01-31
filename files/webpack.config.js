var path = require("path");

module.exports = {
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
    }
}
