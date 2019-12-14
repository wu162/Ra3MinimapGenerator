import os
import sys

from PyQt5.QtGui import QPixmap, QImage
from PyQt5.QtWidgets import QApplication, QMainWindow, QMessageBox
from PIL import Image
from UI import *
from core import GenMiniMap


class MyWindow(QMainWindow, Ui_MainWindow):
    def __init__(self, parent=None):
        super(MyWindow, self).__init__(parent)
        self.setupUi(self)

        self.mapName = ''

        # 左边背景图
        self.status = False

        # 填充地图列表
        maplist, self.mapPath = self.getMaps()
        self.maplistBox.addItems(maplist)

        # 默认勾选
        self.style1.setChecked(True)
        self.option2.setChecked(True)
        self.style = "grass"
        self.option = "2"

        # 单选按钮绑定值
        self.style1.value = "grass"
        self.style2.value = "fortress"
        self.style3.value = "snow"
        self.option1.value = "1"
        self.option2.value = "2"
        self.option3.value = "3"
        self.option4.value = "4"

        # 响应事件
        self.style1.toggled.connect(self.onRadioButton)
        self.style2.toggled.connect(self.onRadioButton)
        self.style3.toggled.connect(self.onRadioButton)
        self.option1.toggled.connect(self.onRadioButton)
        self.option2.toggled.connect(self.onRadioButton)
        self.option3.toggled.connect(self.onRadioButton)
        self.option4.toggled.connect(self.onRadioButton)
        self.saveButton.clicked.connect(self.onSave)
        self.viewButton.clicked.connect(self.onView)
        self.saveButton.setEnabled(False)

    def onView(self):
        if len(self.maplistBox.selectedItems()) > 0:
            self.mapName = self.maplistBox.selectedItems()[0].text()
        else:
            QMessageBox.information(self, "警告",
                                    self.tr("您还没有选择地图!"))
            return
        filepath = os.path.join(self.mapPath, self.mapName, self.mapName) + '.tga'
        try:
            self.res = GenMiniMap(filepath, self.style, int(self.option))
            self.status = True
            img = QImage(self.res, self.res.shape[0], self.res.shape[1], 3 * self.res.shape[0], QImage.Format_RGB888)
            self.mapPanel.setPixmap(QPixmap(img))
        except Exception as e:
            self.status = False
            QMessageBox.information(self, "发生错误",
                                    self.tr("无法生成该小地图!"))
        finally:
            self.saveButton.setEnabled(self.status)

    def onSave(self):
        self.saveMap(self.res)

    def onRadioButton(self):
        radioButton = self.sender()
        if radioButton.isChecked():
            if radioButton.value == "grass":
                self.style = "grass"
                return
            if radioButton.value == "fortress":
                self.style = "fortress"
                return
            if radioButton.value == "snow":
                self.style = "snow"
                return
            if radioButton.value == "1":
                self.option = "1"
                return
            if radioButton.value == "2":
                self.option = "2"
                return
            if radioButton.value == "3":
                self.option = "3"
                return
            if radioButton.value == "4":
                self.option = "4"
                return

    def getMaps(self):
        env_dist = os.environ
        # 先不管路径是否存在
        mapPath = env_dist.get('appdata')
        mapPath = env_dist.get('appdata') + '\Red Alert 3\Maps'
        maps = []
        if not os.path.exists(mapPath):
            QMessageBox.information(self, "警告",
                                    self.tr("检测不到红警3地图文件夹!"))
            return maps, mapPath
        dir = os.walk(mapPath)

        for path, dir_list, file_list in dir:
            for dir_name in dir_list:
                if os.path.exists(os.path.join(mapPath, dir_name, dir_name) + '.tga'):
                    maps.append(dir_name)

        return maps, mapPath

    def saveMap(self, mapdata):
        minimap = Image.fromarray(mapdata)
        savePath = os.path.join(self.mapPath, self.mapName, self.mapName)
        savePath = savePath + '_art.tga'
        if not os.path.exists(savePath):
            minimap.save(savePath)
        else:
            button = QMessageBox.question(self, "保存",
                                          self.tr("是否替换原来的小地图?"),
                                          QMessageBox.Ok | QMessageBox.Cancel,
                                          QMessageBox.Ok)
            if button == QMessageBox.Ok:
                os.remove(savePath)
                minimap.save(savePath)


if __name__ == '__main__':
    QtCore.QCoreApplication.setAttribute(QtCore.Qt.AA_EnableHighDpiScaling)  # 适应分辨率
    app = QApplication(sys.argv)
    myWin = MyWindow()
    myWin.show()
    sys.exit(app.exec_())
