const mongoose = require('mongoose');

// Account
const accountSchema = new mongoose.Schema({
    user: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'User',
    },
    amount: {
      type: Number,
      default: 0,
    },
  });
  
  const Account = mongoose.model('Account', accountSchema);
  
  module.exports = Account;