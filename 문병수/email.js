const nodemailer = require('nodemailer');

const smtpTransport = nodemailer.createTransport({
    service: "Naver",
    auth: {
        user: "mb3ss@naver.com",
        pass: "qudtnWkdWkdaos1!"
    },
    tls: {
        rejectUnauthorized: false
    }
  });

  module.exports={
      smtpTransport
  }