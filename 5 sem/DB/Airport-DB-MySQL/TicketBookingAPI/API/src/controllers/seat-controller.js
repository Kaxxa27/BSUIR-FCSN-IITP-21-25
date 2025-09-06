const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const SeatController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM seat');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM seat WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("seat not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { number, row, seatclass_id, plane_id} = req.body;

        try {
            const query = `
            INSERT INTO seat(number, \`row\`, seatclass_id, plane_id)
            VALUES (?, ?, ?, ?)
            `;

            await pool.query(query, [number, row, seatclass_id, plane_id]);

            res.status(201).json({ message: 'seat created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const { number, row, seatclass_id, plane_id } = req.body;

        try {
            const query = `
                UPDATE seat
                SET number = ?, \`row\` = ?, seatclass_id = ?, plane_id = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [number, row, seatclass_id, plane_id, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("seat not found"));
            }

            res.status(200).json({ message: 'seat updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM seat WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("seat not found"));
            }

            res.status(200).json({ message: 'seat deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = SeatController;