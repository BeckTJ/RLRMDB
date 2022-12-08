import React from 'react';
import {NavigationContainer, Tab} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import {View, Text} from 'react-native';
import Distillation from './DistillationHomeScreen';
import RunLog from './RunLog';
import MaterialSelection from './MaterialSelection';
import Reports from './Reports';

const Stack = createNativeStackNavigator();

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator>
        <Stack.Screen name="Distillation" component={Distillation} />
        <Stack.Screen name="Material Selection" component={MaterialSelection} />
        <Stack.Group otherParam={'Material Selection'}>
          <Stack.Screen name="Run Log" component={RunLog} />
        </Stack.Group>
        <Stack.Screen name="Reports" component={Reports} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
