pipes = []

apiCall = function (req) {
    return "Hello " + req;
}

wrap = function (req, next) {
    console.log("start", req)
    var response = next(req);
    console.log("end", response)
    return response;
}

tryWrap = function (req, next) {
    console.log("trying", req)
    return next(req);
}

addPipe = function (pipe) {
    if (pipes.length === 0) {
        pipes.push((req) => pipe(req, apiCall))
    } else {
        let previousPipe = pipes[pipes.length - 1]
        pipes.push((req) => pipe(req, previousPipe))
    }
}

build = function () {
    return pipes[pipes.length - 1]
}

addPipe(tryWrap);
addPipe(wrap);

mainPipe = build()

mainPipe("World")