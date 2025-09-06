const ApiError = require('../exceptions/api-error');
const pool = require('../db/connection-pool');

const PassportController = {
    getAll: async (req, res, next) => {
        try {
            const [rows] = await pool.query('SELECT * FROM passport');
            res.json(rows);
        } catch (e) {
            next(e);
        }
    },

    getById: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [rows] = await pool.query('SELECT * FROM passport WHERE id = ?', [id]);

            if (!rows.length) {
                return next(ApiError.NotFoundError("passport not found"));
            }

            res.status(200).json(rows[0]);
        } catch (e) {
            next(e);
        }
    },

    create: async (req, res, next) => {
        const { first_name, last_name, birth_date, 
            pass_number, identification_number, issue_date, expiry_date, 
            nationality_id, gender_id, country_id, user_id } = req.body;

        try {
            const query = `
                INSERT INTO passport(first_name, last_name, birth_date, 
                    pass_number, identification_number, issue_date, expiry_date, 
                    nationality_id, gender_id, country_id, user_id)
                VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)
            `;

            await pool.query(query, [first_name, last_name, birth_date, 
                pass_number, identification_number, issue_date, expiry_date, 
                nationality_id, gender_id, country_id, user_id]);

            res.status(201).json({ message: 'passport created successfully' });
        } catch (e) {
            next(e);
        }
    },

    update: async (req, res, next) => {
        const id = req.params.id;
        const { first_name, last_name, birth_date, 
            pass_number, identification_number, issue_date, expiry_date, 
            nationality_id, gender_id, country_id, user_id } = req.body;

        try {
            const query = `
                UPDATE passport
                SET first_name = ?, last_name = ?, birth_date = ?, 
                pass_number = ?, identification_number = ?, issue_date = ?, expiry_date = ?, 
                nationality_id = ?, gender_id = ?, country_id = ?, user_id = ?
                WHERE id = ?
            `;

            const [result] = await pool.query(query, [first_name, last_name, birth_date, 
                pass_number, identification_number, issue_date, expiry_date, 
                nationality_id, gender_id, country_id, user_id, id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("passport not found"));
            }

            res.status(200).json({ message: 'passport updated successfully' });
        } catch (e) {
            next(e);
        }
    },

    delete: async (req, res, next) => {
        const id = req.params.id;

        try {
            const [result] = await pool.query('DELETE FROM passport WHERE id = ?', [id]);

            if (!result.affectedRows) {
                return next(ApiError.NotFound("passport not found"));
            }

            res.status(200).json({ message: 'passport deleted successfully' });
        } catch (e) {
            next(e);
        }
    },

}
module.exports = PassportController;