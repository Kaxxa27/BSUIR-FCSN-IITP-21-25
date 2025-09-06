const express = require('express');
const router = express.Router();
const authorizeController = require('../controllers/authorize.controller');


router.post('/register', authorizeController.registerUser);
router.post('/login', authorizeController.loginUser);

module.exports = router;
