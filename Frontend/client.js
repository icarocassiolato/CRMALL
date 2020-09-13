var bodyParser = require('body-parser');

var express = require('express');
var app = express();
var session = require('express-session');
var flash = require('connect-flash');
const serverHost = 'http://localhost:49306/api';

app.use(flash());
app.use(session({ cookie: { maxAge: 60000 },
    secret: 'woot',
    resave: false,
    saveUninitialized: false}));

app.use(express.static(__dirname + "/public"));
app.use(bodyParser.json());


app.get(serverHost + '/cliente',function (req, res) {
    Test.find(function (err,docs) {
        console.log(docs);
        res.json(docs);
    });
});

app.post(serverHost + '/cliente',function(req, res){
    var test = new Test(req.body);
    test.save(function(err,doc) {
        if (err) {
            return res.send(err);
        } else {
            req.flash('Sucesso', 'O nome foi alterado');
        }
        res.json(doc);
    });
});

app.delete(serverHost + '/cliente/:id' ,function (req, res) {
    Test.remove({_id: req.params.id}, function(err, doc) {
        if (err) {
            return res.send(err);
        }
        res.json(doc);
    });
});

app.get(serverHost + '/cliente/:id', function (req, res) {
    Test.findOne({ _id: req.params.id},{"name" :1,"email": 1}, function(err, doc) {
        if (err) {
            return res.send(err);
        }
        res.json(doc);
    });
});

app.put(serverHost + '/cliente/:id',function (req,res) {
    Test.findOne({ _id: req.params.id }, function(err, doc) {
        if (err) {
            return res.send(err);
        }
        for (prop in req.body) {
            doc[prop] = req.body[prop];
        }
        doc.save(function(err) {
            if (err) {
                return res.send(err);
            }
            res.json(doc);
        });
    });
});

app.listen(3000);
console.log("Aplicação rodando na porta 3000");
