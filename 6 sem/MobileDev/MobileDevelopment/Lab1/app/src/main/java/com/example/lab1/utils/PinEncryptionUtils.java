package com.example.lab1.utils;

import android.content.Context;
import android.util.Base64;
import android.util.Log;
import androidx.security.crypto.EncryptedSharedPreferences;
import androidx.security.crypto.MasterKeys;
import java.nio.charset.StandardCharsets;
import java.util.Arrays;
import javax.crypto.spec.SecretKeySpec;

public class PinEncryptionUtils {

    private static final int SHIFT = 3;
    private static final String KEY_NAME = "pin_key";
    private SecretKeySpec generatedKey;

    public PinEncryptionUtils() {
        generatedKey = generatePassKey();
        Log.d("PinEncryptionUtils", "Generated key: " + generatedKey);
    }

    public byte[] encryptPin(String pin) {
        byte[] keyBytes = generatedKey.getEncoded();
        byte[] pinBytes = pin.getBytes(StandardCharsets.UTF_8);
        byte[] encryptedBytes = new byte[pinBytes.length];
        for (int i = 0; i < pinBytes.length; i++) {
            encryptedBytes[i] = (byte) (pinBytes[i] ^ keyBytes[i % keyBytes.length]);
        }
        return encryptedBytes;
    }

    public String decryptPin(byte[] encryptedPin) {
        byte[] keyBytes = generatedKey.getEncoded();
        byte[] decryptedBytes = new byte[encryptedPin.length];
        for (int i = 0; i < encryptedPin.length; i++) {
            decryptedBytes[i] = (byte) (encryptedPin[i] ^ keyBytes[i % keyBytes.length]);
        }
        return new String(decryptedBytes, StandardCharsets.UTF_8);
    }

    private SecretKeySpec generatePassKey() {
        byte[] keyBytes = new byte[16];
        Arrays.fill(keyBytes, (byte) SHIFT);
        return new SecretKeySpec(keyBytes, "AES");
    }

    public void savePinToSecureStorage(Context context, String pin) {
        try {
            byte[] encryptedPin = encryptPin(pin);
            String masterKeyAlias = MasterKeys.getOrCreate(MasterKeys.AES256_GCM_SPEC);
            EncryptedSharedPreferences sharedPreferences = (EncryptedSharedPreferences) EncryptedSharedPreferences.create(
                    "SecureStorage",
                    masterKeyAlias,
                    context,
                    EncryptedSharedPreferences.PrefKeyEncryptionScheme.AES256_SIV,
                    EncryptedSharedPreferences.PrefValueEncryptionScheme.AES256_GCM
            );
            sharedPreferences.edit().putString(KEY_NAME, Base64.encodeToString(encryptedPin, Base64.DEFAULT)).apply();
        } catch (Exception e) {
            Log.e("PinEncryptionUtils", "Error saving PIN to secure storage", e);
        }
    }

    public String loadPinFromSecureStorage(Context context) {
        try {
            String masterKeyAlias = MasterKeys.getOrCreate(MasterKeys.AES256_GCM_SPEC);
            EncryptedSharedPreferences sharedPreferences = (EncryptedSharedPreferences) EncryptedSharedPreferences.create(
                    "SecureStorage",
                    masterKeyAlias,
                    context,
                    EncryptedSharedPreferences.PrefKeyEncryptionScheme.AES256_SIV,
                    EncryptedSharedPreferences.PrefValueEncryptionScheme.AES256_GCM
            );
            String encryptedPinString = sharedPreferences.getString(KEY_NAME, null);
            if (encryptedPinString != null) {
                byte[] encryptedPin = Base64.decode(encryptedPinString, Base64.DEFAULT);
                return decryptPin(encryptedPin);
            } else {
                return null;
            }
        } catch (Exception e) {
            Log.e("PinEncryptionUtils", "Error loading PIN from secure storage", e);
            return null;
        }
    }
}
