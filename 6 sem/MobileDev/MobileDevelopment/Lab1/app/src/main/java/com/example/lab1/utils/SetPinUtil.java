package com.example.lab1.utils;

import static com.example.lab1.utils.BiometricUtils.showBiometricConfirmationDialog;

import android.content.Intent;
import android.widget.TextView;
import android.widget.Toast;

import com.example.lab1.AuthActivity;
import com.example.lab1.MainActivity;

public class SetPinUtil {

    public static void setPin(AuthActivity activity, TextView pinInputTextView, String storedPin, int MAX_PIN_LENGTH) {
        String pin = pinInputTextView.getText().toString();
        if (pin.length() == MAX_PIN_LENGTH) {
            if (storedPin == null) {
                activity.pinEncryptionUtils.savePinToSecureStorage(activity, pin);
                activity.storedPin = pin;
                Toast.makeText(activity.getApplicationContext(), "PIN set successfully", Toast.LENGTH_SHORT).show();
            } else {
                showBiometricConfirmationDialog(activity, pin);
            }
        } else {
            Toast.makeText(activity.getApplicationContext(), "PIN must contain " + MAX_PIN_LENGTH + " digits", Toast.LENGTH_SHORT).show();
        }
    }

    public static void login(AuthActivity activity, TextView pinInputTextView, String storedPin, int MAX_PIN_LENGTH) {
        String enteredPin = pinInputTextView.getText().toString();
        if (storedPin == null) {
            Toast.makeText(activity.getApplicationContext(), "PIN not set", Toast.LENGTH_SHORT).show();
        } else if (enteredPin.length() == MAX_PIN_LENGTH && enteredPin.equals(storedPin)) {
            Toast.makeText(activity.getApplicationContext(), "PIN correct. Logging in...", Toast.LENGTH_SHORT).show();
            Intent intent = new Intent(activity, MainActivity.class);
            activity.startActivity(intent);
            activity.finish();
        } else {
            Toast.makeText(activity.getApplicationContext(), "Incorrect PIN", Toast.LENGTH_SHORT).show();
        }
    }

    public static void appendDigit(AuthActivity activity, TextView pinInputTextView, int MAX_PIN_LENGTH, TextView view) {
        if (pinInputTextView.getText().length() < MAX_PIN_LENGTH) {
            CharSequence digit = view.getText();
            pinInputTextView.append(digit);
        }
    }

    public static void deleteDigitPin(TextView pinInputTextView) {
        String currentPin = pinInputTextView.getText().toString();
        if (!currentPin.isEmpty()) {
            String newPin = currentPin.substring(0, currentPin.length() - 1);
            pinInputTextView.setText(newPin);
        }
    }

    public static void deleteAllPin(TextView pinInputTextView) {
        pinInputTextView.setText("");
    }
}