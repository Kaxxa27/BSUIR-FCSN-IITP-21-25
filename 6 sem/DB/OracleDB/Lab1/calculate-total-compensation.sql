CREATE OR REPLACE FUNCTION calculate_total_compensation(
    p_monthly_salary NUMBER,
    p_bonus NUMBER
) 
RETURN NUMBER 
IS
    v_total_compensation NUMBER;
    v_bonus_percentage NUMBER;
    
    ex_invalid_salary EXCEPTION;
    ex_invalid_bonus EXCEPTION;
BEGIN
    IF p_monthly_salary < 0 THEN
        RAISE ex_invalid_salary;
    END IF;
    
    IF p_bonus < 0 THEN
        RAISE ex_invalid_bonus;
    END IF;
    
    IF NOT REGEXP_LIKE(p_bonus, '^[[:digit:]]+$') THEN
        RAISE VALUE_ERROR;
    END IF;
    
    v_bonus_percentage := p_bonus / 100.0;
    
    v_total_compensation := (1 + v_bonus_percentage) * 12 * p_monthly_salary;
    
    RETURN v_total_compensation;
EXCEPTION
    WHEN ex_invalid_salary THEN
        DBMS_OUTPUT.PUT_LINE('Месячная зарплата должна быть неотрицательным числом.');
        RETURN NULL;
    WHEN ex_invalid_bonus THEN
        DBMS_OUTPUT.PUT_LINE('Процент годовых премиальных не может быть отрицательным.');
        RETURN NULL;
    WHEN VALUE_ERROR THEN
        DBMS_OUTPUT.PUT_LINE('Процент годовых премиальных должен быть целым числом.');
        RETURN NULL;
    WHEN OTHERS THEN
        DBMS_OUTPUT.PUT_LINE('Произошла ошибка:');
        RETURN NULL;
END;
/

DECLARE 
    v_salary NUMBER := 1;
    v_bonus NUMBER := 100;
BEGIN    
    DBMS_OUTPUT.PUT_LINE(calculate_total_compensation(v_salary, v_bonus));
END;
/
