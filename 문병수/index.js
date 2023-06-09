const https = require("https");
const fs = require("fs");
const express = require("express");
const cors = require("cors");
var morgan = require('morgan');
var logger = require('./winston.js');
const ejs = require('ejs');

const { pool } = require("./db");
const { smtpTransport } = require('./email');

const app = express();
const PORT = 8080;
var privateKey = fs.readFileSync("/etc/letsencrypt/live/j8c210.p.ssafy.io/privkey.pem");
var certificate = fs.readFileSync("/etc/letsencrypt/live/j8c210.p.ssafy.io/cert.pem");
var ca = fs.readFileSync("/etc/letsencrypt/live/j8c210.p.ssafy.io/chain.pem");
const credentials = { key: privateKey, cert: certificate, ca: ca };


app.use(cors());
app.use(express.json());
app.use(morgan('combined', {stream: logger.stream})); 

var generateRandom = function (min, max) {
    var ranNum = Math.floor(Math.random()*(max-min+1)) + min;
    return ranNum;
  }

app.get("/", async (req, res) => {
	return res.sendFile("index.html", {
		root : "./html"
	});
});

app.get("/img", async (req, res) => {
	return res.sendFile("sejong.jpg", {
		root : "./file"
	});
});

app.get("/api/download", async (req, res) => {
	return res.download("/home/ubuntu/db/file/trap.jpg", "trap.jpg");
});

app.post("/api/email", async (req, res) => {
	let state = false;
	let msg = "email >> error";
	const number = generateRandom(111111,999999)
	const sendEmail = req.body.email;
	console.log(req.body);
	let emailTemplete;
	ejs.renderFile("./authMail.ejs", {authCode : number}, function (err, data) {
		if(err){console.log(err)}
		emailTemplete = data;
	});


	const mailOptions = {
		from: "mb3ss@naver.com",
		to: sendEmail,
		subject: "[메만추] 인증 관련 이메일 입니다",
		//text: "오른쪽 숫자 6자리를 입력해주세요 : " + number
		html: emailTemplete,
	};

	const result = await smtpTransport.sendMail(mailOptions, (error, responses) => {
		if (error) {
			mse = error;
			console.log(msg);
			return res.json({
				success : state, message : msg
			});
		} else {
			/* 클라이언트에게 인증 번호를 보내서 사용자가 맞게 입력하는지 확인! */
			msg = number;
			console.log(msg);
			state = true;
			return res.json({
				success : state, message : msg
			});
		}
		smtpTransport.close();
	});
});

app.get("/api/ranks", async (req, res) => {
	let state = false;
	let index = 0;
	let msg = "ranks >> error";
	let status = 400;
	let ranks = [];
	try {
		// asc 오름차순
		// desc 내림차순
		const data = await pool.query("SELECT * FROM unityInfo.ranks WHERE map = ? ORDER BY time asc", req.query.map);
		state = true;
		msg = "ranks >> okisbacon"; 
		status = 200;
		ranks = data[0];
	} catch (error) {
		msg = error;
	}

	console.log(msg);
	logger.info(msg);
	return res.json({
		success : state, index : index, message : msg, status : status, ranks : ranks
	});
});

app.get("/api/ranksd", async (req, res) => {
	let state = false;
	let index = 0;
	let msg = "ranks >> error";
	let status = 400;
	let ranks = [];
	try {
		// asc 오름차순
		// desc 내림차순
		const data = await pool.query("SELECT * FROM unityInfo.ranks WHERE map = ? ORDER BY time desc", req.query.map);
		state = true;
		msg = "ranks >> okisbacon";
		status = 200;
		ranks = data[0];
	} catch (error) {
		msg = error;
	}

	console.log(msg);
	logger.info(msg);
	return res.json({
		success : state, index : index, message : msg, status : status, ranks : ranks
	});
});


// app.get("/api/login", async (req, res) => {
// 	let state = false;
// 	let index = 1;
// 	let msg = "user/:id >> error";
// 	let status = 400;
// 	let name = "";
// 	try {
// 		const data = await pool.query("SELECT * FROM unityInfo.user WHERE id = ?", req.params.id);
// 		if (data[0].length > 0) {
// 			state = true;
// 			msg = "okisbacon";
// 			name = data[0][0].NAME;
// 			status = 200;
// 		}
// 	} catch (error) {
// 		msg = error;
// 	}

// 	console.log(msg);

// 	return res.json({
// 		success : state, index : index, message : msg, status : status, name : name
// 	});
// });

app.post('/api/ranksins', async(req, res) => {
	let state = false;
	let index = 2;
	let msg = "rankins >> error";
	let status = 400;
	console.log(req.body);
	logger.info(JSON.stringify(req.body));
	try {
		const data = await pool.query("INSERT INTO unityInfo.ranks (id, name, map, time) VALUES (?, ?, ?, ?)",
					[req.body.id, req.body.name, req.body.map, req.body.time]);
		state = true;
		msg = "rankins >> okisbacon";
		status = 200;

	} catch (error) {
		msg = error;
	}

	console.log(msg);
	logger.info(msg);

	return res.json({
		success : state, index : index, message : msg, status : status
	});
})

app.post('/api/signup', async(req, res) => {
	var state = false;
	let index = 3;
	var msg = "signUp >> Name duplicated";
	var status = 400;
	console.log(req.body);
	logger.info(JSON.stringify(req.body));
	const name = await pool.query("SELECT * FROM unityInfo.user WHERE NAME = ?", req.body.NAME);

	if(name[0].length == 0){
		try {
			const data = await pool.query("INSERT INTO unityInfo.user (ID, NAME, PASS) VALUES (?, ?, ?)", 
						[req.body.ID, req.body.NAME, req.body.PASS]);
			state = true;
			msg = "signUp >> okisbacon";
			status = 200;
		} 
		catch (error) {
			msg = "signUp >> ID duplicated";
		}
	} 

	console.log(msg);
	logger.info(msg);

	return res.json({
		success : state, index : index, message : msg, status : status
	});
})

app.post('/api/login', async(req, res) => {
	let state = false;
	let index = 4;
	let msg = "login >> ID does not exist";
	let status = 400;
	let name = "";
	console.log(req.body);
	logger.info(JSON.stringify(req.body));
	const user = await pool.query("SELECT * FROM unityInfo.user WHERE ID = ?", req.body.ID);

	if(user[0].length > 0){
		if(user[0][0].PASS == req.body.PASS){
			state = true;
			msg = "login >> okisbacon";
			name = user[0][0].NAME;
			status = 200;
		}
		else{
			msg = "login >> PW incorrect";
		}
	}

	console.log(msg);
	logger.info(msg);

	return res.json({
		success : state, index : index, message : msg, status : status, name : name
	});
})

// app.listen(PORT, () => console.log(`this server listening on ${PORT}`));
// http.createServer(app).listen(PORT, () => console.log(`this server listening on ${PORT}`));
https.createServer(credentials, app).listen(PORT, () => console.log(`this server listening on ${PORT}`));