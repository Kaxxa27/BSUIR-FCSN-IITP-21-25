package com.example.lab1;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Switch;
import android.widget.TextView;
import androidx.appcompat.app.AppCompatActivity;
import android.widget.GridLayout;
import androidx.appcompat.widget.AppCompatButton;

import com.google.firebase.messaging.FirebaseMessaging;
import com.google.firebase.messaging.RemoteMessage;

import net.objecthunter.exp4j.Expression;
import net.objecthunter.exp4j.ExpressionBuilder;

public class MainActivity extends AppCompatActivity implements SensorEventListener{

    private SensorManager sensorManager;
    private Sensor accelerometer;
    private static final float THRESHOLD = 30.0f;
    private boolean tiltedUp = false;

    private Switch switcher;
    private DarkModeManager darkModeManager;

    private BtnHistoryManager historyManager;
    private TextView resultField;
    private TextView operationField;
    private Calculator calculator;

    private static final String KEY_RESULT = "Result";
    private static final String KEY_OPERATION = "Pperation";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        sensorManager = (SensorManager) getSystemService(Context.SENSOR_SERVICE);
        accelerometer = sensorManager.getDefaultSensor(Sensor.TYPE_ACCELEROMETER);
        sensorManager.registerListener((SensorEventListener) this, accelerometer, SensorManager.SENSOR_DELAY_NORMAL);

        resultField = findViewById(R.id.resultField);
        operationField = findViewById(R.id.operationField);

        calculator = new Calculator();
        historyManager = new BtnHistoryManager();

        GridLayout gridLayout = findViewById(R.id.buttonGrid);
        for (int i = 0; i < gridLayout.getChildCount(); i++) {
            View view = gridLayout.getChildAt(i);
            if (view instanceof AppCompatButton) {
                AppCompatButton button = (AppCompatButton) view;
                button.setOnClickListener((View v) -> onButtonClick(button.getText().toString()));
            }
        }

        historyManager.readLastResult(new BtnHistoryManager.OnResultLoadedListener() {
            @Override
            public void onResultLoaded(ResultPress result) {
                if (result != null) {
                    resultField.setText(result.getResult());
                    operationField.setText(result.getOperation());
                }
            }
        });

//        FirebaseMessaging.getInstance().send(new RemoteMessage.Builder("@gcm.googleapis.com")
//                .setMessageId(Integer.toString(123))
//                .addData("title", "Загрузка завершена")
//                .addData("message", "Приложение успешно загружено")
//                .build());

        if (savedInstanceState != null) {
            resultField.setText(savedInstanceState.getString(KEY_RESULT));
            operationField.setText(savedInstanceState.getString(KEY_OPERATION));
        }

        switcher = findViewById(R.id.switcher);

        if (switcher != null) {
            darkModeManager = new DarkModeManager(switcher);
        }
    }

    @Override
    public void onSensorChanged(SensorEvent event) {
        float xAcceleration = event.values[0]; // Ускорение по оси X
        float yAcceleration = event.values[1]; // Ускорение по оси Y
        float zAcceleration = event.values[2]; // Ускорение по оси Z

        Log.d("Acceleration", "X: " + xAcceleration + ", Y: " + yAcceleration + ", Z: " + zAcceleration);

        if(xAcceleration > 10) {
            calculator.processButtonClick("AC", operationField, resultField);
        }
        else if(xAcceleration < -10) {
            calculator.processButtonClick("=", operationField, resultField);
        }
    }

    @Override
    public void onAccuracyChanged(Sensor sensor, int accuracy) {

    }

    protected void onButtonClick(String buttonText) {
        calculator.processButtonClick(buttonText, operationField, resultField);
    }

    @Override
    protected void onSaveInstanceState(Bundle outState) {
        super.onSaveInstanceState(outState);
        outState.putString(KEY_RESULT, resultField.getText().toString());
        outState.putString(KEY_OPERATION, operationField.getText().toString());
    }
}
