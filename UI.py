# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'GenMiniMap.ui'
#
# Created by: PyQt5 UI code generator 5.13.0
#
# WARNING! All changes made in this file will be lost!
from base64 import b64decode
import os

from PyQt5 import QtCore, QtGui, QtWidgets
from PyQt5.QtGui import QIcon

from icon import Icon


class Ui_MainWindow(object):
    def setupUi(self, MainWindow):
        MainWindow.setObjectName("MainWindow")
        MainWindow.resize(600, 400)

        #加载图标
        with open('tmp.ico', 'wb') as tmp:
            tmp.write(b64decode(Icon().img))
        MainWindow.setWindowIcon(QIcon('tmp.ico'))
        os.remove('tmp.ico')

        self.centralwidget = QtWidgets.QWidget(MainWindow)
        self.centralwidget.setObjectName("centralwidget")
        self.mapPanel = QtWidgets.QLabel(self.centralwidget)
        self.mapPanel.setGeometry(QtCore.QRect(40, 40, 256, 256))
        self.mapPanel.setObjectName("mapPanel")
        self.maplistBox = QtWidgets.QListWidget(self.centralwidget)
        self.maplistBox.setGeometry(QtCore.QRect(330, 20, 101, 151))
        self.maplistBox.setObjectName("maplistBox")
        self.saveButton = QtWidgets.QPushButton(self.centralwidget)
        self.saveButton.setGeometry(QtCore.QRect(450, 280, 100, 41))
        self.saveButton.setObjectName("genButton")
        self.viewButton = QtWidgets.QPushButton(self.centralwidget)
        self.viewButton.setGeometry(QtCore.QRect(330, 280, 100, 41))
        self.viewButton.setObjectName("viewButton")
        self.styleGroup = QtWidgets.QGroupBox(self.centralwidget)
        self.styleGroup.setGeometry(QtCore.QRect(449, 20, 131, 151))
        self.styleGroup.setObjectName("styleGroup")
        self.style1 = QtWidgets.QRadioButton(self.styleGroup)
        self.style1.setGeometry(QtCore.QRect(20, 20, 68, 13))
        self.style1.setObjectName("radioButton")
        self.style2 = QtWidgets.QRadioButton(self.styleGroup)
        self.style2.setGeometry(QtCore.QRect(20, 60, 68, 13))
        self.style2.setObjectName("style2")
        self.style3 = QtWidgets.QRadioButton(self.styleGroup)
        self.style3.setGeometry(QtCore.QRect(20, 110, 68, 13))
        self.style3.setObjectName("style3")
        self.optionGroup = QtWidgets.QGroupBox(self.centralwidget)
        self.optionGroup.setGeometry(QtCore.QRect(330, 180, 251, 80))
        self.optionGroup.setObjectName("optionGroup")
        self.option1 = QtWidgets.QRadioButton(self.optionGroup)
        self.option1.setGeometry(QtCore.QRect(20, 20, 80, 13))
        self.option1.setObjectName("option1")
        self.option2 = QtWidgets.QRadioButton(self.optionGroup)
        self.option2.setGeometry(QtCore.QRect(140, 20, 80, 13))
        self.option2.setObjectName("option2")
        self.option3 = QtWidgets.QRadioButton(self.optionGroup)
        self.option3.setGeometry(QtCore.QRect(20, 50, 80, 13))
        self.option3.setObjectName("option3")
        self.option4 = QtWidgets.QRadioButton(self.optionGroup)
        self.option4.setGeometry(QtCore.QRect(140, 50, 80, 13))
        self.option4.setObjectName("option4")
        MainWindow.setCentralWidget(self.centralwidget)
        self.menubar = QtWidgets.QMenuBar(MainWindow)
        self.menubar.setGeometry(QtCore.QRect(0, 0, 600, 18))
        self.menubar.setObjectName("menubar")
        self.menu = QtWidgets.QMenu(self.menubar)
        self.menu.setObjectName("menu")
        MainWindow.setMenuBar(self.menubar)
        self.statusbar = QtWidgets.QStatusBar(MainWindow)
        self.statusbar.setObjectName("statusbar")
        MainWindow.setStatusBar(self.statusbar)
        self.actionabout = QtWidgets.QAction(MainWindow)
        self.actionabout.setObjectName("actionabout")
        self.menu.addAction(self.actionabout)
        self.menubar.addAction(self.menu.menuAction())

        self.retranslateUi(MainWindow)
        QtCore.QMetaObject.connectSlotsByName(MainWindow)

    def retranslateUi(self, MainWindow):
        _translate = QtCore.QCoreApplication.translate
        MainWindow.setWindowTitle(_translate("MainWindow", "小地图生成器"))
        self.mapPanel.setText(_translate("MainWindow", ""))
        self.viewButton.setText(_translate("MainWindow", "预览"))
        self.saveButton.setText(_translate("MainWindow", "保存"))
        self.styleGroup.setTitle(_translate("MainWindow", "风格"))
        self.style1.setText(_translate("MainWindow", "草原"))
        self.style2.setText(_translate("MainWindow", "要塞"))
        self.style3.setText(_translate("MainWindow", "雪地"))
        self.optionGroup.setTitle(_translate("MainWindow", "结果类型"))
        self.option1.setText(_translate("MainWindow", "骨架"))
        self.option2.setText(_translate("MainWindow", "地块"))
        self.option3.setText(_translate("MainWindow", "地块+骨架"))
        self.option4.setText(_translate("MainWindow", "地块+二层边缘"))
        self.menu.setTitle(_translate("MainWindow", "帮助"))
        self.actionabout.setText(_translate("MainWindow", "关于..."))
