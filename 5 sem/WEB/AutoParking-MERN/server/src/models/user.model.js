const mongoose = require("mongoose");
const Account = require('./account.model');

const userScheme = new mongoose.Schema({
    username:{
        type: String,
        minlength: [1, 'Username cannot be shorter than 1 characters'],
        maxlength: [45, 'Username cannot be longer than 45 characters'],
        trim: true,
    },
    password:{
        type: String,
        required: true,
        minlength: [3, 'The password cannot be shorter than 3 characters'],
    },
    email: {
        type: String,
        required: [true, 'Email is required'],
        unique: true,
        trim: true,
        match: [/\S+@\S+\.\S+/, 'Invalid email format'],
    },
    age:{
        type: Number,
        min: [0, 'The age cannot be less than 0'],
        max: [150, 'The age cannot be more than 150'],
    },
});

userScheme.post('findOneAndDelete', async (doc) => {
    try {
        if (doc) {
            await Account.deleteOne({ user: doc._id });
        }
    } catch (error) {
        console.error(error);
    }
});

const User = mongoose.model('User', userScheme);

module.exports = User;