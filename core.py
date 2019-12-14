import cv2
import numpy as np
from PIL import Image


def GenMiniMap(file, style, option):
    color=getColor(style)

    grey, diff = getData(file)
    cannyedge, cannyInverse, cannyedgeOriginal = getEdge(grey)
    if option == 1:
        return getBones(cannyedgeOriginal)
    labels, blocknum, avg = getBlock(cannyInverse, grey)

    resR, resG, resB = colorBlock(labels, grey, diff, avg, blocknum,color)

    if option == 3:
        resR, resG, resB = addEdge(resR, resG, resB, cannyedgeOriginal)

    if option == 4:
        resR, resG, resB = addHigherEdge(resR, resG, resB, grey, avg)

    # imshow(resG.astype(np.uint8))
    resR = np.reshape(resR, (resR.shape[0], resR.shape[1], 1))
    resG = np.reshape(resG, (resG.shape[0], resG.shape[1], 1))
    resB = np.reshape(resB, (resB.shape[0], resB.shape[1], 1))
    res = np.concatenate((resR, resG, resB), 2)
    res = res.astype(np.uint8)
    # imshow(res.astype(np.uint8))

    return res


def getData(filename):
    img = Image.open(filename)
    width, height = img.size
    img = img.resize((width * 2, height * 2), Image.ANTIALIAS)
    data = np.asarray(img, dtype=np.int32)
    fR = data[:, :, 0]
    fG = data[:, :, 1]
    fB = data[:, :, 2]

    diff = fB - fR
    diff = diff > 10
    greyImage = img.convert('L')
    grey = np.array(greyImage)
    grey[diff] = 0
    return grey, diff


def getEdge(grey):
    # cannyedgeOriginal = cv2.Canny(grey, 40, 70)
    # cannyedgeOriginal = cv2.Sobel(grey, cv2.CV_8U, 0, 1, ksize=5)

    grey = cv2.GaussianBlur(grey, (5, 5), 2)
    cannyedgeOriginal = cv2.Canny(grey, 10, 20, L2gradient=True)

    kernel = cv2.getStructuringElement(cv2.MORPH_ELLIPSE, (5, 5))
    cannyedge = cv2.dilate(cannyedgeOriginal, kernel)
    cannyInverse = cannyedge == 0
    # imshow(cannyedge)

    # kernel = cv2.getStructuringElement(cv2.MORPH_ELLIPSE, (2, 2))
    # cannyedgeOriginal = cv2.dilate(cannyedgeOriginal, kernel)
    return cannyedge, cannyInverse, cannyedgeOriginal


def getBlock(cannyInverse, grey):
    retval, labels, stats, centroids = cv2.connectedComponentsWithStats(cannyInverse.astype(np.uint8))
    # imshow(labels == 1)
    # imshow(labels == 2)
    # imshow(labels == 3)
    # imshow(labels == 4)
    # imshow(labels == 5)
    # imshow(labels == 3)
    # imshow(labels == 3)
    blocknum = retval - 1;
    avgDul = np.zeros([blocknum, 1])
    for i in range(blocknum):
        area = grey[labels == i]
        # print(area.shape[0])
        if area.shape[0] < 30:
            continue
        else:
            areasum = sum(area)
            avgDul[i] = areasum / area.shape[0];

    # print(avgDul)
    avgDul = np.round(avgDul)
    avgDul = np.unique(avgDul)
    avgDul = np.sort(avgDul)
    # print(avgDul)

    # print(blocknum)
    avg = np.array(avgDul[0])
    last = avgDul[0]
    for i in range(1, avgDul.shape[0]):
        if avgDul[i] - avgDul[i - 1] > 4:
            last = avgDul[i]
            avg = np.append(avg, avgDul[i], )

    # print(avg)

    return labels, blocknum, avg


def colorBlock(labels, grey, diff, avg, blocknum,color):
    # color = np.array([[72, 97, 104], [94, 95, 53], [151, 130, 85], [176, 158, 113]])
    m, n = grey.shape
    resR = np.zeros([m, n])
    resG = np.zeros([m, n])
    resB = np.zeros([m, n])
    for i in range(blocknum):
        mask = labels == i + 1
        area = grey[mask]
        areasum = sum(area)
        areatemp = areasum / area.shape[0]
        place = (np.abs(avg - areatemp)).argmin()
        restemp = 1 * mask
        if place > 0:
            resR = resR + restemp * color[place, 0]
            resG = resG + restemp * color[place, 1]
            resB = resB + restemp * color[place, 2]
        else:
            resR = np.multiply(resR, np.invert(diff))
            resG = np.multiply(resG, np.invert(diff))
            resB = np.multiply(resB, np.invert(diff))

            resR = resR + diff * color[place, 0]
            resG = resG + diff * color[place, 1]
            resB = resB + diff * color[place, 2]

    kernel = cv2.getStructuringElement(cv2.MORPH_ELLIPSE, (7, 7))
    resR = cv2.dilate(resR, kernel)
    resG = cv2.dilate(resG, kernel)
    resB = cv2.dilate(resB, kernel)

    for i in range(m):
        for j in range(n):
            if resR[i, j] < 0.001:
                place = (np.abs(avg - grey[i, j])).argmin()
                resR[i, j] = color[place, 0]
                resG[i, j] = color[place, 1]
                resB[i, j] = color[place, 2]

    return resR, resG, resB


def addHigherEdge(resR, resG, resB, grey, avg):

    if (avg.shape[0] >= 3):
        grey[grey < avg[1] + 10] = 0
    grey = cv2.GaussianBlur(grey, (5, 5), 2)
    edge = cv2.Canny(grey, 90, 100, L2gradient=True)

    kernel = cv2.getStructuringElement(cv2.MORPH_ELLIPSE, (3, 3))
    edge = cv2.dilate(edge, kernel)

    edgeInverse = edge == 0
    edgeInverse = 1 * edgeInverse
    resRwithEdge = np.multiply(resR, edgeInverse)
    resGwithEdge = np.multiply(resG, edgeInverse)
    resBwithEdge = np.multiply(resB, edgeInverse)
    return resRwithEdge, resGwithEdge, resBwithEdge


def getBones(cannyedge):
    cannyedge=np.invert(cannyedge)
    cannyedge = cannyedge.astype(np.uint8)
    cannyedge = np.reshape(cannyedge, (cannyedge.shape[0], cannyedge.shape[1], 1))
    res = np.concatenate((cannyedge, cannyedge, cannyedge), 2)
    res = res.astype(np.uint8)
    return res


def getColor(style):
    if style == 'grass':
        color = np.array([[72, 97, 104], [94, 95, 53], [151, 130, 85], [176, 158, 113], [181, 186, 145]])
        return color
    elif style == 'fortress':
        color = np.array([[72, 97, 104], [88, 98, 123], [116, 131, 152]])
        return color
    elif style == 'snow':
        color = np.array([[72, 97, 104], [145, 146, 151], [162, 165, 174], [183, 188, 191]])
        return color


def addEdge(resR, resG, resB, edge):
    edgeInverse = edge == 0
    edgeInverse = 1 * edgeInverse
    resRwithEdge = np.multiply(resR, edgeInverse)
    resGwithEdge = np.multiply(resG, edgeInverse)
    resBwithEdge = np.multiply(resB, edgeInverse)
    return resRwithEdge, resGwithEdge, resBwithEdge
