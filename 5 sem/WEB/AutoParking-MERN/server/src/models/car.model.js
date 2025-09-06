const mongoose = require('mongoose');

// Car
const carSchema = new mongoose.Schema({
  owner: {
    type: mongoose.Schema.Types.ObjectId,
    ref: 'User',
  },
  mark: {
    type: String,
    required: true,
  },
  model: {
    type: String,
    required: true,
  },
  license_plate: {
    type: String,
    required: true,
  },
});

const Car = mongoose.model('Car', carSchema);

module.exports = Car;
