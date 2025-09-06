package com.example.lab1;

import android.view.View;
import android.widget.Switch;
import android.widget.TextView;

import androidx.appcompat.app.AppCompatDelegate;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.database.DataSnapshot;
import com.google.firebase.database.DatabaseReference;
import com.google.firebase.database.FirebaseDatabase;

import net.objecthunter.exp4j.*;

public class Calculator {

    private BtnHistoryManager buttonPressHistoryManager;

    public Calculator() {
        // Инициализируем объект ButtonPressHistoryManager
        buttonPressHistoryManager = new BtnHistoryManager();
    }
    public void processButtonClick(String buttonText, TextView operationField, TextView resultField) {
        switch (buttonText) {
            case "sqrt":
            case "log2":
            case "log":
            case "sin":
            case "cos":
                operationField.append(buttonText + "(");
                break;
            case "x^y":
                operationField.append("^");
                break;
            case "AC":
                resultField.setText("");
                operationField.setText("");
                break;
            case "back":
                String s = operationField.getText().toString();
                if (!s.isEmpty()) {
                    operationField.setText(s.substring(0, s.length() - 1));
                }
                break;
            case "=":
                String operationText = operationField.getText().toString();
                String resultText = "";
                if (!operationText.isEmpty()) {
                    try {
                        Expression expression = new ExpressionBuilder(operationText).build();
                        double result = expression.evaluate();
                        resultText = Double.toString(result);
                        resultField.setText(resultText);
                    } catch (Exception exception) {
                        resultField.setText("Invalid input");
                    }
                }

                buttonPressHistoryManager.saveResultPress(resultText, operationText);
                break;
            default:
                operationField.append(buttonText);
                break;
        }

        buttonPressHistoryManager.saveButtonPress(buttonText);
    }
}

