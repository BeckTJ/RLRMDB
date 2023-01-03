import React, {useState, useEffect} from 'react';
import {View, Text, StyleSheet, FlatList} from 'react-native';
import LargeButton from '../components/LargeButton';
import MaterialSelection from './MaterialSelection';

export default Distillation = ({navigation, route}) => {
  const distillationOptions = [
    'Raw Material',
    'Run Log',
    'Sample Submit',
    'Reports',
  ];
  handleHomeScreenPress = selection => {
    if (selection === 'Reports') {
      navigation.push(selection, {data: selection});
    } else {
      navigation.push('Material Selection', {data: selection});
    }
  };

  return (
    <View style={styles.container}>
      <View style={styles.title}>
        <Text style={styles.header}>RAVEN</Text>
        <Text style={styles.text}>Distillation</Text>
      </View>
      <FlatList
        numColumns={2}
        ItemSeparatorComponent={<View style={{margin: '5%'}} />}
        columnWrapperStyle={{justifyContent: 'space-evenly'}}
        style={styles.buttons}
        data={distillationOptions}
        renderItem={({item}) => (
          <LargeButton
            onPress={() => handleHomeScreenPress(item)}
            key={item.index}
            name={item}
          />
        )}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    margin: '10%',
  },
  title: {
    alignItems: 'center',
    justifyContent: 'center',
  },
  header: {
    fontSize: 84,
    fontStyle: 'italic',
    fontWeight: 'bold',
  },
  text: {
    fontSize: 62,
  },
  buttons: {
    marginTop: '15%',
    flex: 1,
    flexDirection: 'column',
  },
});
