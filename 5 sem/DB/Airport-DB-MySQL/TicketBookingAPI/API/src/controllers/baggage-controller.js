const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const BaggageController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM baggage');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM baggage WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("baggage not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { weight, ticket_id } = req.body;

        try {
            const query = `
                INSERT INTO baggage(weight, ticket_id)
                VALUES (?, ?)
            `;

            await pool.query(query, [weight, ticket_id]);

            res.status(201).json({ message: 'baggage created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const {weight, ticket_id } = req.body;

        try {
            const query = `
                UPDATE baggage
                SET weight = ?, ticket_id = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [weight, ticket_id, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("baggage not found"));
            }

            res.status(200).json({ message: 'baggage updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM baggage WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("baggage not found"));
            }

            res.status(200).json({ message: 'baggage deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = BaggageController;