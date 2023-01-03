import React from 'react';
import {NavigationContainer, Tab} from '@react-navigation/native';
import {createNativeStackNavigator} from '@react-navigation/native-stack';
import {View, Text} from 'react-native';
import Distillation from './screens/DistillationHomeScreen';
import RunLog from './screens/RunLog';
import MaterialSelection from './screens/MaterialSelection';
import Reports from './screens/Reports';
import ProductInfo from './screens/ProductInfo';

const Stack = createNativeStackNavigator();

export default function App() {
  return (
    <NavigationContainer>
      <Stack.Navigator>
        <Stack.Screen name="Distillation" component={Distillation} />
        <Stack.Screen name="Material Selection" component={MaterialSelection} />
        <Stack.Screen name="Product Information" component={ProductInfo} />
        <Stack.Screen name="Run Log" component={RunLog} />
        <Stack.Screen name="Reports" component={Reports} />
      </Stack.Navigator>
    </NavigationContainer>
  );
}
