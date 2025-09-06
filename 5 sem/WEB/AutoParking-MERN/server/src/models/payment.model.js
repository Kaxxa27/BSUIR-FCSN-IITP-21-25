const mongoose = require('mongoose');

// Payment
const paymentSchema = new mongoose.Schema({
    owner: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'User',
      required: true,
    },
    park: {
      type: mongoose.Schema.Types.ObjectId,
      ref: 'ParkingSpot',
    },
    amount: {
      type: Number,
      required: true,
    },
    is_paid: {
      type: Boolean,
      default: false,
    },
    receipt_date: {
      type: Date,
      default: Date.now,
    },
    repayment_date: {
      type: Date,
      default: function () {
          const currentDate = new Date();
          currentDate.setMonth(currentDate.getMonth() + 1);
          return currentDate;
      },
    },
});
  
const Payment = mongoose.model('Payment', paymentSchema);

module.exports = Payment;