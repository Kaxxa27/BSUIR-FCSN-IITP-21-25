const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const GenderController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM gender');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM gender WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("gender not found"));
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
                INSERT INTO gender(name)
                VALUES (?)
            `;

            await pool.query(query, [name]);

            res.status(201).json({ message: 'gender created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const { name } = req.body;

        try {
            const query = `
                UPDATE gender
                SET name = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [name, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("gender not found"));
            }

            res.status(200).json({ message: 'gender updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM gender WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("gender not found"));
            }

            res.status(200).json({ message: 'gender deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = GenderController;