require('dotenv').config();
const jwt = require('jsonwebtoken');
const bcrypt = require('bcrypt');
const User = require('../models/user.model');

// Registration 
exports.registerUser = async (req, res) => {
    try {
        const hashedPassword = await bcrypt.hash(req.body.password, 10);

        const newUser = new User({
            username: req.body.username,
            password: hashedPassword,
            email: req.body.email,
            name: req.body.name,
            age: req.body.age,
        });

        await newUser.save();

        const token = jwt.sign(
            {   
                userId: newUser._id, 
                email: newUser.email, 
                name: newUser.name, 
                age: newUser.age  
            }, 
            process.env.SECRET_JWT_KEY, 
            { expiresIn: '30d' }
        );

        res.status(201).json({ user: newUser, token });
    } catch (error) {
        res.status(error.name === 'ValidationError' ? 400 : 500).json({
            name: error.name,
            message: error.message,
        });
    }
};

// Authentication
exports.loginUser = async (req, res) => {
    try {
        const user = await User.findOne({ email: req.body.email });

        if (!user) {
            return res.status(401).json({ message: 'Authentication failed. User not exist.' });
        }

        const isPasswordValid = await bcrypt.compare(req.body.password, user.password);

        if (!isPasswordValid) {
            return res.status(401).json({ message: 'Authentication failed. Wrong password!' });
        }

        const token = jwt.sign(
            {   
                userId: user._id, 
                email: user.email, 
                name: user.name, 
                age: user.age  
            }, 
            process.env.SECRET_JWT_KEY, 
            { expiresIn: '30d' }
        );

        res.status(200).json({ user, token });
    } catch (error) {
        res.status(500).json({
            name: error.name,
            message: error.message,
        });
    }
};