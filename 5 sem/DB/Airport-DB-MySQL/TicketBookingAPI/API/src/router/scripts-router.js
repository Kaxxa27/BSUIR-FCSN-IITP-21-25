const router = require('express').Router();
const controller = require('../controllers/scripts-controller');
const authMiddleware = require('../middleware/auth-middleware');
const staffMiddleware = require('../middleware/staff-middleware');

router.get('/someWhere', controller.someWhere);
router.get('/having', controller.having);
router.get('/aggregateFunc', controller.aggregateFunc);
router.get('/allSumOfTicketPrice', controller.allSumOfTicketPrice);
router.get('/theMostPopularNationality', controller.theMostPopularNationality);

module.exports = router;