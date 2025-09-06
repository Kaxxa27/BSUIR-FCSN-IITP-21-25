package com.example.lab1;

import com.google.firebase.messaging.FirebaseMessagingService;
import com.google.firebase.messaging.RemoteMessage;

public class ResultPress {
    private String result;
    private String operation;

    public ResultPress() {

    }

    public ResultPress(String result, String operation) {
        this.result = result;
        this.operation = operation;
    }

    public String getResult() {
        return result;
    }

    public String getOperation() {
        return operation;
    }
}

