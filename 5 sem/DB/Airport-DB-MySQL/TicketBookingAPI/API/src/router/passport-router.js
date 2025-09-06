const router = require('express').Router();
const controller = require('../controllers/passport-controller');
const authMiddleware = require('../middleware/auth-middleware');
const staffMiddleware = require('../middleware/staff-middleware');

router.get('/', controller.getAll);
router.get('/:id', controller.getById);
router.post('/', [authMiddleware, staffMiddleware], controller.create);
router.put('/:id', [authMiddleware, staffMiddleware], controller.update);
router.delete('/:id', [authMiddleware, staffMiddleware], controller.delete);

module.exports = router;