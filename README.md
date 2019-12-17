# 红警3小地图生成器
一个小项目，自动读取地图文件夹下的地图及相应地图编辑器生成的tga来生成一张小地图，但是由于技术不够，以及这个tga文件损失了很多高度信息，
没法准确生成大部分图的斜坡和海岸，故放弃这部分功能，之后有时间可能会结合地图编辑器导出的高度图来完善

## 讨论&建议
可以去百度贴吧的[帖子](https://tieba.baidu.com/p/6392454370)里发帖

## 风格
每种风格都应相应的陆地层数限制
   * 草地    4层
   * 雪地    3层
   * 要塞    2层
   
## 结果类型
最后一个选项即地块+二层悬崖可能会出现一些问题，一般选择地块或者地块+骨架

## 如何获得好的结果
各种高度尽量按照官方地图，即
   * 水面高度200
   * 第一层陆地210
   * 第二层陆地270，
   
   以此类推，高度差不要大小

## 相关问题
   * 若打开后出现 **检测不到红警3地图文件夹** 请先用红警3万能工具修复注册表
   * 若出现 **无法生成该小地图** 可能是地形过于复杂或者超过相应风格的层数，也可能是高度差与官方相差较大
