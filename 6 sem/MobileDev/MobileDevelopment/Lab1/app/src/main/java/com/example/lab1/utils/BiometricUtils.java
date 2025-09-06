package com.example.lab1.utils;

import android.content.Context;
import android.content.Intent;
import android.widget.Toast;
import androidx.biometric.BiometricPrompt;
import androidx.core.content.ContextCompat;

import com.example.lab1.AuthActivity;
import com.example.lab1.MainActivity;

public class BiometricUtils {

    public static void biometricAuth(AuthActivity activity) {
        BiometricPrompt biometricAuthPrompt = new BiometricPrompt(activity,
                ContextCompat.getMainExecutor(activity),
                new BiometricPrompt.AuthenticationCallback() {
                    @Override
                    public void onAuthenticationError(int errorCode, CharSequence errString) {
                        super.onAuthenticationError(errorCode, errString);
                        Toast.makeText(activity.getApplicationContext(), "Auth error: " + errString, Toast.LENGTH_SHORT).show();
                    }

                    @Override
                    public void onAuthenticationSucceeded(BiometricPrompt.AuthenticationResult result) {
                        super.onAuthenticationSucceeded(result);
                        Toast.makeText(activity.getApplicationContext(), "Auth succeeded", Toast.LENGTH_SHORT).show();
                        activity.startActivity(new Intent(activity, MainActivity.class));
                        activity.finish();
                    }

                    @Override
                    public void onAuthenticationFailed() {
                        super.onAuthenticationFailed();
                        Toast.makeText(activity.getApplicationContext(), "Auth failed", Toast.LENGTH_SHORT).show();
                    }
                });

        BiometricPrompt.PromptInfo authPromptInfo = new BiometricPrompt.PromptInfo.Builder()
                .setTitle("Login into application")
                .setSubtitle("Using fingerprint")
                .setNegativeButtonText("Use alternative password")
                .setConfirmationRequired(false)
                .build();

        biometricAuthPrompt.authenticate(authPromptInfo);
    }

    public static void showBiometricConfirmationDialog(AuthActivity activity, String newPin) {
        BiometricPrompt biometricPrompt = new BiometricPrompt(activity,
                ContextCompat.getMainExecutor(activity),
                new BiometricPrompt.AuthenticationCallback() {
                    @Override
                    public void onAuthenticationError(int errorCode, CharSequence errString) {
                        super.onAuthenticationError(errorCode, errString);
                        Toast.makeText(activity.getApplicationContext(), "Auth error: " + errString, Toast.LENGTH_SHORT).show();
                    }

                    @Override
                    public void onAuthenticationSucceeded(BiometricPrompt.AuthenticationResult result) {
                        super.onAuthenticationSucceeded(result);
                        activity.pinEncryptionUtils.savePinToSecureStorage(activity, newPin);
                        activity.storedPin = newPin;
                        Toast.makeText(activity.getApplicationContext(), "PIN updated successfully", Toast.LENGTH_SHORT).show();
                    }

                    @Override
                    public void onAuthenticationFailed() {
                        super.onAuthenticationFailed();
                        Toast.makeText(activity.getApplicationContext(), "Auth failed", Toast.LENGTH_SHORT).show();
                    }
                });

        BiometricPrompt.PromptInfo promptInfo = new BiometricPrompt.PromptInfo.Builder()
                .setTitle("Confirm PIN change")
                .setSubtitle("Using fingerprint to confirm PIN change")
                .setNegativeButtonText("Cancel")
                .build();

        biometricPrompt.authenticate(promptInfo);
    }

    public static void showPin(AuthActivity activity) {
        BiometricPrompt biometricPrompt = new BiometricPrompt(activity,
                ContextCompat.getMainExecutor(activity),
                new BiometricPrompt.AuthenticationCallback() {
                    @Override
                    public void onAuthenticationError(int errorCode, CharSequence errString) {
                        super.onAuthenticationError(errorCode, errString);
                        Toast.makeText(activity.getApplicationContext(), "Auth error: " + errString, Toast.LENGTH_SHORT).show();
                    }

                    @Override
                    public void onAuthenticationSucceeded(BiometricPrompt.AuthenticationResult result) {
                        super.onAuthenticationSucceeded(result);
                        if (activity.storedPin != null) {
                            Toast.makeText(activity.getApplicationContext(), "PIN: " + activity.storedPin, Toast.LENGTH_SHORT).show();
                        } else {
                            Toast.makeText(activity.getApplicationContext(), "PIN not set", Toast.LENGTH_SHORT).show();
                        }
                    }

                    @Override
                    public void onAuthenticationFailed() {
                        super.onAuthenticationFailed();
                        Toast.makeText(activity.getApplicationContext(), "Auth failed", Toast.LENGTH_SHORT).show();
                    }
                });

        BiometricPrompt.PromptInfo promptInfo = new BiometricPrompt.PromptInfo.Builder()
                .setTitle("Confirm your identity")
                .setSubtitle("Using fingerprint to show PIN")
                .setNegativeButtonText("Cancel")
                .build();

        biometricPrompt.authenticate(promptInfo);
    }
}
