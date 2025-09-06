const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const AirportController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM airport');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM airport WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("airport not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { name, code_IATA, city_id } = req.body;

        try {
            const query = `
                INSERT INTO airport(name, code_IATA, city_id)
                VALUES (?, ?, ?)
            `;

            await pool.query(query, [name, code_IATA, city_id]);

            res.status(201).json({ message: 'airport created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const { name, code_IATA, city_id } = req.body;

        try {
            const query = `
                UPDATE airport
                SET name = ?, code_IATA = ?, city_id = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [name, code_IATA, city_id, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("airport not found"));
            }

            res.status(200).json({ message: 'airport updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM airport WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("airport not found"));
            }

            res.status(200).json({ message: 'airport deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = AirportController;