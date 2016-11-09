var app = require('express')();
var server = require('http').Server(app);
var io = require('socket.io')(server);
var leap = require('leapjs');
var util = require('util');

server.listen(8000);

app.get('/', function (req, res) {
  res.sendfile(__dirname + '/index.html');
});

var time = 0;

io.on('connection', function (socket) {
  /*
  leap.loop(function (frame) {
    if (Date.now() - time > 33) {
      var s = JSON.stringify(util.inspect(frame));
      console.log(s);
      socket.emit('frame', s);
      time = Date.now();
    }
    return;
  });*/
  
  //socket.emit('news', { hello: 'world' });
  console.log("connected");
  socket.on('frame', function (data) {
    //console.log("frame received");
    console.log(data);
  });
  
});