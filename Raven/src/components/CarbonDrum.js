import React from 'react';
import {View, Text, TextInput, StyleSheet} from 'react-native';
import Moment from 'moment';

const CarbonDrum = data => {
  const material = data.material;
  Moment.locale('en');
  let installDate,
    daysAllowed,
    daysUsed = 2;

  if (material.carbonDrumRequired !== null) {
    installDate = material.carbonDrumInstallDate;
    daysAllowed = material.carbonDrumDaysAllowed;
  } else if (material.vacuumTrapRequired !== null) {
    installDate = material.vacuumTrapInstallDate;
    daysAllowed = material.vacuumTrapDaysAllowed;
  } else null;

  const daysOnCurrent = Moment(installDate).diff(daysUsed, 'days');

  return (
    <View style={styles.container}>
      <View style={styles.noInput}>
        <Text style={styles.preStartText}>
          Carbon Drum Install Date:
          {Moment(installDate).format('d/MM/yyyy')}
        </Text>
      </View>
      <View style={styles.noInput}>
        <Text style={styles.preStartText}>
          Change out Required:
          {daysAllowed}
        </Text>
      </View>
      <View style={styles.noInput}>
        <Text style={styles.preStartText}>
          Days on Current: {daysOnCurrent}
        </Text>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    marginTop: 10,
    marginLeft: 25,
  },
  noInput: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    alignItems: 'baseline',
  },
  preStartText: {
    fontSize: 20,
    marginRight: 10,
  },
  preStartInput: {
    fontSize: 20,
    borderBottomWidth: 1.5,
    width: 125,
    marginRight: 10,
    paddingBottom: -10,
  },
  drumWeight: {
    flexWrap: 'wrap',
    flexDirection: 'row',
    alignItems: 'baseline',
    marginBottom: -20,
  },
});
export default CarbonDrum;
