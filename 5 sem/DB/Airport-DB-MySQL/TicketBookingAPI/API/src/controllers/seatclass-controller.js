const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const SeatClassController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM seatclass');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM seatclass WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("seatclass not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { name } = req.body;

        try {
            const query = `
                INSERT INTO seatclass(name)
                VALUES (?)
            `;

            await pool.query(query, [name]);

            res.status(201).json({ message: 'seatclass created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const { name } = req.body;

        try {
            const query = `
                UPDATE seatclass
                SET name = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [name, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("seatclass not found"));
            }

            res.status(200).json({ message: 'seatclass updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM seatclass WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("seatclass not found"));
            }

            res.status(200).json({ message: 'seatclass deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = SeatClassController;