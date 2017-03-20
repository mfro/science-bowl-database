export function REST(method, url, data) {
    if (typeof data == 'object')
        data = JSON.stringify(data);

    if (REST.base)
        url = REST.base + url;

    return new Promise((resolve, reject) => {
        var req = new XMLHttpRequest();
        req.addEventListener('load', () => {
            var res;
            let contentType = req.getResponseHeader('Content-Type');
            if (contentType && contentType.indexOf('application/json') != -1)
                res = JSON.parse(req.responseText);

            if (req.status == 200)
                resolve(res);
            else
                reject(res);
        });

        req.open(method, url, true);
        if (REST.token) req.setRequestHeader('Auth-Token', REST.token);

        if (data) {
            req.setRequestHeader('Content-Type', 'application/json');
            req.send(data);
        } else {
            req.send();
        }
    });
}

const key = 'questions';

REST.load = function (callback) {
    REST.base = SERVICE_URL;
    REST.socket = SERVICE_URL.replace('http', 'ws');

    callback();
}
